using CommunityToolkit.Mvvm.Input;
using Plugin.Maui.OCR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillSpliter.ViewModels
{
    public partial class MainPageViewModel:BaseViewModel
    {
        [RelayCommand]
        public async Task CaptureReceiptAsync()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult myPhoto = await MediaPicker.Default.CapturePhotoAsync();
                if (myPhoto != null)
                {
                    //string localFilePath = Path.Combine(FileSystem.CacheDirectory, myPhoto.FileName);
                    using Stream sourceStream = await myPhoto.OpenReadAsync();
                    // Save in Gallery
                    await SaveImage(sourceStream);
                    //using FileStream localFileStream = File.OpenWrite(localFilePath);
                    //await sourceStream.CopyToAsync(localFileStream);
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("No Camera", "Device camera not supported", "Ok");
            }
        }

        public static async Task SaveImage(Stream imageStream)
        {
            using var stream = imageStream;
            using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);

            stream.Position = 0;
            memoryStream.Position = 0;

#if ANDROID
var context = Platform.CurrentActivity;

    if (OperatingSystem.IsAndroidVersionAtLeast(29))
    {
        Android.Content.ContentResolver resolver = context.ContentResolver;
        Android.Content.ContentValues contentValues = new();
        contentValues.Put(Android.Provider.MediaStore.IMediaColumns.DisplayName, "image.png");
        contentValues.Put(Android.Provider.MediaStore.IMediaColumns.MimeType, "image/png");
        contentValues.Put(Android.Provider.MediaStore.IMediaColumns.RelativePath, "DCIM/" + "image");
        Android.Net.Uri imageUri = resolver.Insert(Android.Provider.MediaStore.Images.Media.ExternalContentUri, contentValues);
           
        var os = resolver.OpenOutputStream(imageUri);
        Android.Graphics.BitmapFactory.Options options = new();
        options.InJustDecodeBounds = true;
        var bitmap = Android.Graphics.BitmapFactory.DecodeStream(stream);
        bitmap.Compress(Android.Graphics.Bitmap.CompressFormat.Png, 100, os);
        os.Flush();
        os.Close();
    }
    else
    {
        Java.IO.File storagePath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures);
        string path = System.IO.Path.Combine(storagePath.ToString(), "image.png");
        System.IO.File.WriteAllBytes(path, memoryStream.ToArray());
        var mediaScanIntent = new Android.Content.Intent(Android.Content.Intent.ActionMediaScannerScanFile);
        mediaScanIntent.SetData(Android.Net.Uri.FromFile(new Java.IO.File(path)));
        context.SendBroadcast(mediaScanIntent);
    }

#endif
        }

        [RelayCommand]
        private async Task PickImageAsync()
        {
            try
            {
                var pickResult = await MediaPicker.Default.PickPhotoAsync();
                if (pickResult != null)
                {
                    using var imageAsStream = await pickResult.OpenReadAsync();
                    var imageAsBytes = new Byte[imageAsStream.Length];
                    await imageAsStream.ReadAsync(imageAsBytes);

                    var ocrResult= await OcrPlugin.Default.RecognizeTextAsync(imageAsBytes,true);

                    if (!ocrResult.Success)
                    {
                        await Shell.Current.DisplayAlert("No success", "No OCR possible", "ok");
                        return;
                    }
                    await Shell.Current.DisplayAlert("OCR result",ocrResult.AllText, "ok");

                }
            }
            catch(Exception ex) 
            { 
                await Shell.Current.DisplayAlert(Shell.Current.Title, ex.Message,"ok");
            }
        }
    }
}
