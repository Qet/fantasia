#!/bin/bash +e

# Run all the tests

pushd $PWD
START_DIR=$PWD
for x in `ls -d *.tests`; do
    cd $x
    dotnet test
    cd $START_DIR
done;
popd

