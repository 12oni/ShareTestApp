using System;
using System.IO;
using Foundation;
using MobileCoreServices;
using Social;

namespace ShareExtension
{
    public partial class ShareViewController : SLComposeServiceViewController
    {
        protected ShareViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any additional setup after loading the view.
        }

        public override bool IsContentValid()
        {
            // Do validation of contentText and/or NSExtensionContext attachments here
            return true;
        }

        public override void DidSelectPost()
        {
            // This is called after the user selects Post. Do the upload of contentText and/or NSExtensionContext attachments.
            var description = "";
            var url = "";

            foreach (var extensionItem in ExtensionContext.InputItems)
            {
                if (extensionItem.Attachments != null)
                {
                    foreach (var attachment in extensionItem.Attachments)
                    {
                        if (attachment.HasItemConformingTo(UTType.URL))
                        {
                            attachment.LoadItem(UTType.URL, null, (data, error) =>
                            {
                                var nsUrl = data as NSUrl;
                                url = nsUrl.AbsoluteString;
                                WriteToDebugFile($"URL - {url}");
                                //Save  off the url and description here
                            });
                        }
                    }
                }
                if (!string.IsNullOrWhiteSpace(extensionItem.AttributedContentText.Value))
                {
                    description = extensionItem.AttributedContentText.Value;
                    WriteToDebugFile($"URL description - {description}");
                }
            }
            // Inform the host that we're done, so it un-blocks its UI. Note: Alternatively you could call super's -didSelectPost, which will similarly complete the extension context.
            ExtensionContext.CompleteRequest(new NSExtensionItem[0], null);
        }

        public override SLComposeSheetConfigurationItem[] GetConfigurationItems()
        {
            // To add configuration options via table cells at the bottom of the sheet, return an array of SLComposeSheetConfigurationItem here.
            return new SLComposeSheetConfigurationItem[0];
        }

        private void WriteToDebugFile(string dbgText)
        {
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename = Path.Combine(documents, "Debug.txt");

            if (!File.Exists(filename))
            {
                File.WriteAllText(filename, $"\n{DateTime.Now} - {dbgText}");
            }
            else
            {
                File.AppendAllText(filename, $"\n{DateTime.Now} - {dbgText}");
            }
        }
    }
}
