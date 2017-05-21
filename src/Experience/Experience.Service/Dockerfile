FROM microsoft/dotnet:1.0.5-runtime-jessie
ENV ASPNETCORE_URLS=http://*:80
ENV ASPNETCORE_ENVIRONMENT=production
RUN mkdir /app
WORKDIR /app
ADD ./bin/Debug/netcoreapp1.1/publish/* /app/
CMD dotnet Experience.Service.dll
