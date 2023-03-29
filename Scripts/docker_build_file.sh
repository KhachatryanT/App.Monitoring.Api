docker buildx build --platform linux/amd64 -f ../App.Monitoring.Host/Dockerfile -t monitoring-api:latest --load ../
docker save -o ../Publish/monitoring-api.tar monitoring-api:latest
docker image rm monitoring-api:latest
