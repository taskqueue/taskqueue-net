#!/bin/sh

export EnableNuGetPackageRestore=true 

xbuild mono-build.proj /property:BUILD_VERSION=$1