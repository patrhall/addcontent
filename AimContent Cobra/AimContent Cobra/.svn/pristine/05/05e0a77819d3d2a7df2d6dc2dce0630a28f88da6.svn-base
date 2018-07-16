using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Web.UI;

namespace AIM.Base.Classes
{
    public class RadHandler
    {
        private static AIM.Business.General.DataObjectsDataContext _db = new AIM.Business.General.DataObjectsDataContext(AIM.Business.DataManager.ConnectionString);


        public static void RadEditor_LoadEditorPreference(ref RadEditor editor, Config config)
        {
            string[] imagePath = new string[1];
            string[] documentPath = new string[1];
            string[] flashPath = new string[1];
            string[] mediaPath = new string[1];
            string[] templatePath = new string[1];

            //editor.Scheme = config.editorscheme_config;

            if (config.secureEditing != null && config.secureEditing != "")
            {
                imagePath[0] = config.secureEditingImages;
                documentPath[0] = config.secureEditingDocument;
                flashPath[0] = config.secureEditingFlash;
                mediaPath[0] = config.secureEditingMedia;
            }
            else
            {
                imagePath[0] = config.imagefolder_config;
                documentPath[0] = config.documentfolder_config;
                flashPath[0] = config.flashfolder_config;
                mediaPath[0] = config.mediafolder_config;
            }

            
            templatePath[0] = config.templatefolder_config;

            editor.ImageManager.ViewPaths = imagePath;
            editor.ImageManager.UploadPaths = imagePath;
            editor.ImageManager.DeletePaths = imagePath;
            if(!string.IsNullOrEmpty(config.imagefilters_config))
            {
                string [] imagefilters = config.imagefilters_config.Split(',');
                editor.ImageManager.SearchPatterns = imagefilters;
            }
            editor.ImageManager.MaxUploadFileSize = config.maxuploadbytesize;

            editor.DocumentManager.ViewPaths = documentPath;
            editor.DocumentManager.UploadPaths = documentPath;
            editor.DocumentManager.DeletePaths = documentPath;
            if(!string.IsNullOrEmpty(config.documentfilters_config))
            {
                string [] documentfilters = config.documentfilters_config.Split(',');
                editor.DocumentManager.SearchPatterns = documentfilters;
            }
            editor.DocumentManager.MaxUploadFileSize = config.maxuploadbytesize;

            editor.FlashManager.ViewPaths = flashPath;
            editor.FlashManager.UploadPaths = flashPath;
            editor.FlashManager.DeletePaths = flashPath;
            editor.FlashManager.MaxUploadFileSize = config.maxuploadbytesize;

            editor.MediaManager.ViewPaths = mediaPath;
            editor.MediaManager.UploadPaths = mediaPath;
            editor.MediaManager.DeletePaths = mediaPath;
            editor.MediaManager.MaxUploadFileSize = config.maxuploadbytesize;

            editor.TemplateManager.ViewPaths = templatePath;
            editor.TemplateManager.UploadPaths = templatePath;
            editor.TemplateManager.DeletePaths = templatePath;
            editor.TemplateManager.MaxUploadFileSize = config.maxuploadbytesize;

            editor.CssFiles.Clear();
            editor.CssFiles.Add(config.customerpath + "css/Styles.css");

            editor.ContentFilters = Telerik.Web.UI.EditorFilters.EncodeScripts | EditorFilters.ConvertToXhtml | EditorFilters.ConvertFontToSpan | EditorFilters.IndentHTMLContent | EditorFilters.FixUlBoldItalic | EditorFilters.IECleanAnchors | EditorFilters.FixEnclosingP | EditorFilters.MozEmStrong | EditorFilters.OptimizeSpans;
           
            //TODO: Add filter?!
            //config.imagefilters_config;
            //config.documentfilters_config;
            //dialogConfig.SearchPatterns
        }
    }
}
