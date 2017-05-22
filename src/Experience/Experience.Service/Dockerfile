FROM microsoft/aspnetcore:1.1.2
ENV ASPNETCORE_URLS=http://*:80
ENV ASPNETCORE_ENVIRONMENT=production
RUN mkdir /app
WORKDIR /app
ADD ./bin/Debug/netcoreapp1.1/publish /app/
CMD dotnet Experience.Service.dll
