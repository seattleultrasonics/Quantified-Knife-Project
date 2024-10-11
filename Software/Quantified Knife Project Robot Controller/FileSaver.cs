using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QKPRobot
{
public class FileSaver
    {
        // Variable to store the last directory used
        private string lastDirectory = "";

        public string SaveFileAs()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                // Set the default file name (you can set this to an empty string if you wish)
                saveFileDialog.FileName = "Document1";

                // Set the default file type filter
                saveFileDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";

                // Set the initial directory to the last used directory
                saveFileDialog.InitialDirectory = string.IsNullOrEmpty(lastDirectory) ? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) : lastDirectory;

                // Show the SaveFileDialog
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Save the directory used
                    lastDirectory = Path.GetDirectoryName(saveFileDialog.FileName);

                    // Return the selected file path
                    return saveFileDialog.FileName;
                }
            }

            // Return null or an appropriate value if the user canceled the save operation
            return null;
        }
    }

}
