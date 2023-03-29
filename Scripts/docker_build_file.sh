mkdir ../Publish
docker buildx build --platform linux/arm64/v8 -f ../App.Monitoring.Host/Dockerfile -t monitoring-api:latest --load ../
docker save -o ../Publish/monitoring-api.tar monitoring-api:latest
docker image rm monitoring-api:latest
