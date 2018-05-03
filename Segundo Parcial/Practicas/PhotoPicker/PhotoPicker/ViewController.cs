using System;

using UIKit;
using Photos;
using Foundation;
using AVFoundation;
using PhotoPicker.Enums;

namespace PhotoPicker
{
    //Se agrega un Delegate para controlar el comportamiento de la galería.
    public partial class ViewController : UIViewController, IUIImagePickerControllerDelegate
    {

        #region Class Variables

        //Cada vez que se dé un Tap.
        UITapGestureRecognizer profileTapGesture;
        UITapGestureRecognizer bottomTapGesture;


        //Variable para el botón de Edit.
        bool editModeEnabled;

        ImageEnum enumImage;

        #endregion


        #region Constructors

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        #endregion

        #region Controller Life Cycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.


            InitializeComponents();
        }

        #endregion


        #region User Interactions

        //Método asignado a los TapGesture.
        void ShowOptions(UITapGestureRecognizer gesture)
        {

            if (gesture == profileTapGesture)
                enumImage = ImageEnum.Profile;
            else
                enumImage = ImageEnum.Bottom;


            //Un PopUpMenu
            UIAlertController alert = UIAlertController.Create(null, null, UIAlertControllerStyle.ActionSheet);
            alert.AddAction(UIAlertAction.Create("Open photo library", UIAlertActionStyle.Default, TryOpenPhotoLibrary));
            alert.AddAction(UIAlertAction.Create("Take a photo", UIAlertActionStyle.Default, TryOpenCamera));
            alert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null));


            PresentViewController(alert, animated: true, completionHandler: null);


        }

        private void TryOpenPhotoLibrary(UIAlertAction obj)
        {
            //Verificar que esté disponible el recurso.
            if (!UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.PhotoLibrary))
            {
                //Imprimir un mensaje.

                InvokeOnMainThread(() => {
                
                    ShowMessage(title: "Photo Library not available.", message: "The resource is not available due hardware configuration.", fromViewController: NavigationController);
                
                });

            }
            else
            {
                //Revisar el estado de los permisos, para solicitarlo en dado caso que se requiera.
                CheckPhotoLibraryAuthorizationStatus(PHPhotoLibrary.AuthorizationStatus);
            }
        }

        void CheckPhotoLibraryAuthorizationStatus(PHAuthorizationStatus authorizationStatus)
        {

            switch (authorizationStatus)
            {
                case PHAuthorizationStatus.NotDetermined:
                    // Vamos a pedir el permiso para acceder a la galería.

                    //Recursivo, el parámetro es la función misma. Aquí ya entraría en los siguientes opciones
                    //que lo haya denegado o autorizado, dependiendo de lo que haga el usuario al realizarse
                    //la petición.

                    //Solicitud de los permisos y su callback.
                    PHPhotoLibrary.RequestAuthorization(CheckPhotoLibraryAuthorizationStatus);

                    break;
                case PHAuthorizationStatus.Restricted:
                    // Mostrar un mensaje diciendo que está restringido.

                    InvokeOnMainThread(() => {

                        ShowMessage(title: "Resource not available.", message: "The resource is not available due its restricted use.", fromViewController: NavigationController);
                    });



                    break;
                case PHAuthorizationStatus.Denied:
                    // Mostrar un mensaje diciendo que el usuario denegó el permiso.

                    InvokeOnMainThread(() => {

                        ShowMessage(title: "Resource not available.", message: "The resource is not available due it was denied by you.", fromViewController: NavigationController);
                    });



                    break;
                case PHAuthorizationStatus.Authorized:
                    // Vamos a abrir la galería.


                    //Pasarse al hilo principal, ya que esta parte se ejecuta en un método asíncrono.
                    InvokeOnMainThread(() => {
                    
                        //Inicializar una variable para abrir la librería de imágenes.
                        var imagePickerController = new UIImagePickerController
                        {
                            SourceType = UIImagePickerControllerSourceType.PhotoLibrary,
                            //Se asigna "this" porque aquí se está implementando el delegado.
                            Delegate = this
                                                                          
                        };

                        //Se presenta en pantalla la librería de imágenes.
                        PresentViewController(imagePickerController, true, null);
                    
                    
                    });



                    break;
                default:
                    break;
            }
        }

        void TryOpenCamera(UIAlertAction obj)
        {
            //Verificar que esté disponible el recurso.
            if (!UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.Camera))
            {
                //Imprimir un mensaje.
                InvokeOnMainThread(() => {

                    ShowMessage(title: "Camera not available.", message: "The resource is not available due hardware configuration.", fromViewController: NavigationController);

                });
            }
            else
            {
                //Revisar el estado de los permisos, para solicitarlo en dado caso que se requiera.
                CheckCameraAuthorizationStatus(AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video));
            }
        }

        void CheckCameraAuthorizationStatus(AVAuthorizationStatus aVAuthorizationStatus)
        {
            switch (aVAuthorizationStatus)
            {
                case AVAuthorizationStatus.NotDetermined:

                    //Solicitud del permiso.
                    AVCaptureDevice.RequestAccessForMediaType(AVMediaType.Video, (bool accessGranted) => CheckCameraAuthorizationStatus(AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video)) );

                    break;

                case AVAuthorizationStatus.Restricted:

                    InvokeOnMainThread(() => {

                        ShowMessage(title: "Resource not available.", message: "The resource is not available due its restricted use.", fromViewController: NavigationController);
                    });

                    break;

                case AVAuthorizationStatus.Denied:

                    InvokeOnMainThread(() => {

                        ShowMessage(title: "Resource not available.", message: "The resource is not available due it was denied by you.", fromViewController: NavigationController);
                    });

                    break;

                case AVAuthorizationStatus.Authorized:

                    //Abrir la cámara.

                    InvokeOnMainThread(() => {
                        
                        var imagePickerController = new UIImagePickerController
                        {
                            SourceType = UIImagePickerControllerSourceType.Camera,
                            Delegate = this

                        };

                        PresentViewController(imagePickerController, true, null);


                    });

                    break;


                default:
                    
                    break;
            }
        }


        #region UIImage Picker Controller Delegate

        //Implementar la funcionalidad de "Cancel" y salir de la galería de imágenes.

        [Export("imagePickerControllerDidCancel:")]
        public void Canceled(UIImagePickerController picker)
        {
            picker.DismissViewController(animated: true, completionHandler: null);
        }


        //Acción ejecutada al seleccionar una imagen en la galería.

        [Export("imagePickerController:didFinishPickingMediaWithInfo:")]
        public void FinishedPickingMedia(UIImagePickerController picker, NSDictionary info)
        {
            //Obtener la imagen seleccionada sacándola del diccionario y parsearlo a imagen.
            var image = info[UIImagePickerController.OriginalImage] as UIImage;

            //Asignarlo al control.

            //Guardar la imagen dependiendo desde dónde se llamó.
            switch (enumImage)
            {
                case ImageEnum.Profile:

                    ImgProfile.Image = image;
                    break;

                case ImageEnum.Bottom:

                    ImgBottom.Image = image;
                    break;

                default:

                    break;
            }



            //Cerrar el Picker.
            picker.DismissViewController(animated: true, completionHandler: null);
        }


        #endregion


        void BtnEdit_Clicked(object sender, EventArgs e)
        {
            /*

            if (LblProfile.Hidden)
            {
                //Se vuelven visibles.
                LblProfile.Hidden = LblBottom.Hidden = false;
            }
            else
            {
                LblProfile.Hidden = LblBottom.Hidden = true;

                //El Enabled = false es para que esté deshabilitada la gestura por default.
                //Para el profile.
                profileTapGesture = new UITapGestureRecognizer(ShowOptions) { Enabled = false };
                ViewProfile.AddGestureRecognizer(profileTapGesture);


                //Para el bottom.
                bottomTapGesture = new UITapGestureRecognizer(ShowOptions) { Enabled = false };
                ViewBottom.AddGestureRecognizer(bottomTapGesture);
            }
            */

            editModeEnabled = !editModeEnabled;

            BtnEdit.Title = editModeEnabled ? "Done" : "Edit";
            profileTapGesture.Enabled = bottomTapGesture.Enabled = editModeEnabled;
            LblProfile.Hidden = LblBottom.Hidden = !editModeEnabled;
                

        }

        #endregion


        #region Internal Functionality

        void InitializeComponents()
        {
            //Booleano para el botón edit.
            editModeEnabled = false;

            //A ambos se les pone ese atributo.
            LblProfile.Hidden = LblBottom.Hidden = true;

            //El Enabled = false es para que esté deshabilitada la gestura por default.
            //Para el profile.
            profileTapGesture = new UITapGestureRecognizer(ShowOptions) { Enabled = false };
            ViewProfile.AddGestureRecognizer(profileTapGesture);


            //Para el bottom.
            bottomTapGesture = new UITapGestureRecognizer(ShowOptions) { Enabled = false };
            ViewBottom.AddGestureRecognizer(bottomTapGesture);



            BtnEdit.Clicked += BtnEdit_Clicked;

        }


        //Los mensajes deben mostrarse en el ViewController padre de todos, en
        //este caso, en el NavigationController.
        void ShowMessage(string title, string message, UIViewController fromViewController)
        {
            UIAlertController alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));


            fromViewController.PresentViewController(viewControllerToPresent: alert, animated: true, completionHandler: null);

        }

        #endregion

    }
}
