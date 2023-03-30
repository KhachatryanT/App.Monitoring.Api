#!/bin/bash
mkdir ../Publish
docker save -o ../Publish/monitoring-api.tar monitoring-api:latest
