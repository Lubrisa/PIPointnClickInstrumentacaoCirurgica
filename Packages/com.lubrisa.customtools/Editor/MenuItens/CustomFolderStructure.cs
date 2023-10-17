using System.IO;
using UnityEditor;


namespace CustomEditorFeatures
{
    /// <summary>
    /// Utility class that is responsible to generate the initial project folders.
    /// </summary>
    public static class CustomFolderStructure
    {
#if UNITY_EDITOR
        /// <summary>
        /// A array with every file that needs to be generated in the project start.
        /// </summary>
        private static readonly Folder[] m_folders = new Folder[] {
            new Folder("Art", new Folder[] {
                new Folder("Sprites"),
                new Folder("Materials"),
                new Folder("Textures"),
                new Folder("Models"),
                new Folder("Particles")
            }),
            new Folder("Audio", new Folder[] {
                new Folder("Musics"),
                new Folder("SFXs"),
            }),
            new Folder("Code", new Folder[] {
                new Folder("LegacyClasses"),
                new Folder("MonoBehaviours"),
                new Folder("ScriptableObjects")
            }),
            new Folder("Docs"),
            new Folder("Scenes"),
            new Folder("Prefabs", new Folder[] {
                new Folder("Characters"),
                new Folder("UI"),
                new Folder("Scenery")
            })
        };

        /// <summary>
        /// Method responsible for making every folder in the folders array generate it's own folder.
        /// </summary>
        [MenuItem("Tools/Setup/Generate Initial Folders")]
        public static void GenerateFolders()
        {
            // Verifying if both folders, Project and Plugins, exists.
            // If not, generate both folders as subfolders of the Assets folder.
            if (!Directory.Exists("Assets/ProjectFiles")) AssetDatabase.CreateFolder("Assets", "ProjectFiles");
            if (!Directory.Exists("Assets/PackagesSetup")) AssetDatabase.CreateFolder("Assets", "PackagesSetup");

            // Tells every folder in m_folder to generate it's own folder.
            foreach (Folder folder in m_folders)
            {
                folder.CreateFolder("Assets/ProjectFiles");
            }
        }

        /// <summary>
        /// Class representing a folder in the application files.
        /// </summary>
        private sealed class Folder
        {
            /// <summary>
            /// The folder name.
            /// </summary>
            public string FolderName { get; }

            /// <summary>
            /// The folder's subfolders.
            /// </summary>
            public Folder[] SubFolders { get; }

            public Folder(string name, Folder[] subFolders = null)
            {
                FolderName = name;
                SubFolders = subFolders;
            }

            /// <summary>
            /// Method that creates the folder file in the game files.
            /// </summary>
            /// <param name="root"> The root path of the folder. </param>
            public void CreateFolder(string root)
            {
                // Setting this folder path as the concatenation of the root path with this folder name.
                string path = Path.Combine(root, FolderName);

                // Verifying if the path exists.
                if (!Directory.Exists(path))
                {
                    // Generating the folder with path equals the concatenation of the root path with this folder name.
                    AssetDatabase.CreateFolder(root, FolderName);
                }

                // Verifying if this folder has subfolders.
                if (SubFolders is not null)
                {
                    foreach (Folder folder in SubFolders)
                    {
                        // Makes the subfolder generate it's own folder passing this folder path.
                        folder.CreateFolder(path);
                    }
                }
            }
        }
#endif
    }
}