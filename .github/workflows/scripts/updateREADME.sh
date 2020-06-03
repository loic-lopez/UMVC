TAG=$1

echo "Release version: $TAG"

RELEASE_VERSION="${TAG:1}" # escape the "v" of "v1.0.0"
SEARCH='\$VERSION\$'

sed "s/${SEARCH}/${RELEASE_VERSION}/g" README.template.md > README.md

