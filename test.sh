#!/bin/sh +e

# Run all the tests

cd ports.tests
dotnet test
