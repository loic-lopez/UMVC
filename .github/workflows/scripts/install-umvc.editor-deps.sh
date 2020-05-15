unzip -o UMVC.Core.Build.zip -d UMVC.Core.Build

moveDllToDir() {
  echo "Moving dependency $1 to $2"
  mv "$1" "$2"
}


# player deps
moveDllToDir "UMVC.Core.Build/UMVC.Core.Exceptions.dll" "Assets/UMVC/PlayerDependencies"
moveDllToDir "UMVC.Core.Build/UMVC.Core.MVC.dll" "Assets/UMVC/PlayerDependencies"

# Editor deps
moveDllToDir "UMVC.Core.Build/UMVC.Core.Components.dll" "Assets/UMVC/Editor/EditorDependencies"
moveDllToDir "UMVC.Core.Build/UMVC.Core.Generation.dll" "Assets/UMVC/Editor/EditorDependencies"
moveDllToDir "UMVC.Core.Build/UMVC.Core.Templates.dll" "Assets/UMVC/Editor/EditorDependencies"