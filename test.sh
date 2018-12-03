#!/bin/sh +e

# Run all the tests

pushd `pwd`
for x in `ls -d *.tests`; do
    cd $x
    dotnet test
done;

