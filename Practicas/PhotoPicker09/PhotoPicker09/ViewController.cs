using System;

using UIKit;
using Photos;

using Foundation;
using AVFoundation;

namespace PhotoPicker09
{
    public partial class ViewController : UIViewController, IUIImagePickerControllerDelegate
    {
        #region Class Variables
        UITapGestureRecognizer profileTapGesture;
        UITapGestureRecognizer coverTapGesture;

        #endregion
        void InitializeComponents()
        {
            lblEdit.Hidden = LblCover.Hidden = true;
            profileTapGesture = new UITapGestureRecognizer(Opciones) { Enabled = true };
            ProfileView.AddGestureRecognizer(profileTapGesture);
            coverTapGesture = new UITapGestureRecognizer(Opciones) { Enabled = true };
            CoverView.AddGestureRecognizer(coverTapGesture);

        }
        #region Constructor
        protected ViewController(IntPtr handle) : base(handle)
        {
            
        }
        #endregion

        #region Controller Life Cycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitializeComponents();
            BtnEdit.Clicked += BtnEdit_Action;
        }
        #endregion
        void BtnEdit_Action(object sender, EventArgs e){
            //profileTapGesture.Enabled = coverTapGesture.Enabled = editModeEnabled;
            //lblEdit.Hidden = LblCover.Hidden = !editModeEnabled;
            if(LblCover.Hidden==true)
            {
                LblCover.Hidden = false;
                lblEdit.Hidden = false;

            }
            else
            {
                LblCover.Hidden = true;
                lblEdit.Hidden = true;

            }
        }
        #region User Interactions

        void Opciones(UITapGestureRecognizer gesture)
        {

            var alert = UIAlertController.Create(null, null, UIAlertControllerStyle.ActionSheet);
            alert.AddAction(UIAlertAction.Create("Abrir Camera", UIAlertActionStyle.Default, Action => AbrirCamara()));
            alert.AddAction(UIAlertAction.Create("Abrir Galeria", UIAlertActionStyle.Default, Action => AbrirGaleria()));
            alert.AddAction(UIAlertAction.Create("Cancelar", UIAlertActionStyle.Cancel, null));
            PresentViewController(alert, true,  null);

        }

        #endregion

        void AbrirCamara()
        {
            if (!UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.Camera))
            {
                ShowMessage("Error", "Camera no disponible", NavigationController);
                return;
            }
            CheckCameraAtuhorizationStatus(AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video));


        }
        void CheckCameraAtuhorizationStatus(AVAuthorizationStatus authorizationStatus){
            switch (authorizationStatus)
            {
                case AVAuthorizationStatus.NotDetermined:
                    AVCaptureDevice.RequestAccessForMediaTypeAsync(AVMediaType.Video);
                    break;
                case AVAuthorizationStatus.Restricted:
                    InvokeOnMainThread(() => ShowMessage("Fail", "Acceso Restringido", NavigationController));
                    break;
                case AVAuthorizationStatus.Denied:
                    InvokeOnMainThread(() => ShowMessage("Fail", "Acceso Denegado", NavigationController));
                    break;
                case AVAuthorizationStatus.Authorized:
                    InvokeOnMainThread(() =>
                    {
                        var Camera = new UIImagePickerController
                        {
                            SourceType = UIImagePickerControllerSourceType.Camera,
                            Delegate = this
                        };
                        PresentViewController(Camera, true, null);
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        void AbrirGaleria()
        {
            if (!UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.PhotoLibrary))
            {
                ShowMessage("Error", "Galeria no disponible", NavigationController);
                return;
            }
            CheckPhotoLibraryAuthorizationStatus(PHPhotoLibrary.AuthorizationStatus);


        }
        void CheckPhotoLibraryAuthorizationStatus(PHAuthorizationStatus authorizationStatus)
        {
            switch (authorizationStatus)
            {
                case PHAuthorizationStatus.NotDetermined:
                    //permiso para abrir la camara
                    PHPhotoLibrary.RequestAuthorization(CheckPhotoLibraryAuthorizationStatus);
                    break;
                case PHAuthorizationStatus.Restricted:
                    //mensaje de restringido
                    InvokeOnMainThread(() =>
                    {
                        ShowMessage("Recurso no disponible", "La app no tiene permiso para acceder al recurso", NavigationController);
                    });
                    break;
                case PHAuthorizationStatus.Denied:
                    //el usuario denego el recurso
                    InvokeOnMainThread(() =>
                    {
                        ShowMessage("Recurso no disponible", "El usuario denego el recurso", NavigationController);
                    });
                    break;
                case PHAuthorizationStatus.Authorized:
                    //Abrir galeria
                    InvokeOnMainThread(() =>
                    {
                        var imagePickerController = new UIImagePickerController
                        {
                            SourceType = UIImagePickerControllerSourceType.PhotoLibrary,
                            Delegate = this
                        };
                        PresentViewController(imagePickerController, true, null);
                    });

                    break;
                default:
                    break;
            }
        }



        #region UIImage Picker Controller elegate
        [Export("imagePickerController:didFinishPickingMediaWithInfo:")]
        public void FinishedPickingMedia(UIImagePickerController picker, NSDictionary info)
        {
            var image = info[UIImagePickerController.OriginalImage] as UIImage;
            ImgProfile.Image = image;
            ImgCover.Image = image;
            picker.DismissViewController(true,null);
        }
           [Export("imagePickerControllerDidCancel:")]
        public void Canceled(UIImagePickerController picker)
        {
            picker.DismissViewController(true,null);

        }

            
        #endregion
   

        #region Internal Functionality

        void ShowMessage(string title,string messages, UIViewController fromViewController){
            var alert = UIAlertController.Create(title, messages, UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("Oktl" ,UIAlertActionStyle.Default,null ));
            fromViewController.PresentViewController(alert,true,null);
        }
        
        #endregion
    }
}
