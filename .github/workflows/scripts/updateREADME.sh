TAG=$1

echo "Release version: $TAG"

RELEASE_VERSION="$(echo ${TAG} | cut -c 2-)" # escape the "v" of "v1.0.0"
SEARCH='\$VERSION\$'

echo "Updating README.md to version: $TAG..."
sed "s/${SEARCH}/${RELEASE_VERSION}/g" README.template.md > README.md
echo "Done updating README.md to version: $TAG!"

