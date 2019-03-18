FROM microsoft/dotnet:2.2-aspnetcore-runtime AS Base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build

# Install node and npm
ENV NODE_VERSION 8.12.0
ENV NODE_DOWNLOAD_SHA 3df19b748ee2b6dfe3a03448ebc6186a3a86aeab557018d77a0f7f3314594ef6
ENV NODE_DOWNLOAD_URL https://nodejs.org/dist/v$NODE_VERSION/node-v$NODE_VERSION-linux-x64.tar.gz

RUN wget "$NODE_DOWNLOAD_URL" -O nodejs.tar.gz \
	&& echo "$NODE_DOWNLOAD_SHA  nodejs.tar.gz" | sha256sum -c - \
	&& tar -xzf "nodejs.tar.gz" -C /usr/local --strip-components= 1 \
	&& rm nodejs.tar.gz \
	&& ln -s /usr/local/bin/node /usr/local/bin/nodejs

RUN curl -sL https://deb.nodesource.com/setup_6.x |  bash -
RUN apt-get install -y nodejs

# Restore dotnet before build to allow for caching
WORKDIR /

COPY Alpha.Travel.Application/Alpha.Travel.Application.csproj /src/Alpha.Travel.Application/
COPY Alpha.Travel.Common/Alpha.Travel.Common.csproj /src/Alpha.Travel.Common/
COPY Alpha.Travel.Core/Alpha.Travel.Core.csproj /src/Alpha.Travel.Core/
COPY Alpha.Travel.Domain/Alpha.Travel.Domain.csproj /src/Alpha.Travel.Domain/
COPY Alpha.Travel.Infrastructure/Alpha.Travel.Infrastructure.csproj /src/Alpha.Travel.Infrastructure/
COPY Alpha.Travel.Persistence/Alpha.Travel.Persistence.csproj /src/Alpha.Travel.Persistence/
COPY Alpha.Travel.WebApi/Alpha.Travel.WebApi.csproj /src/Alpha.Travel.WebApi/
COPY Alpha.Travel.WebApi.Host/Alpha.Travel.WebApi.Host.csproj /src/Alpha.Travel.WebApi.Host/

RUN dotnet restore /src/Alpha.Travel.WebApi.Host/Alpha.Travel.WebApi.Host.csproj

# Copy source files and build
COPY . /src

RUN dotnet build /src/Alpha.Travel.WebApi.Host/Alpha.Travel.WebApi.Host.csproj --no-restore -c Release
RUN dotnet publish /src/Alpha.Travel.WebApi.Host/Alpha.Travel.WebApi.Host.csproj --no-restore -c Release -o /app

# Copy compiled app to runtime container
FROM base AS final
COPY --from= build / app.
CMD["dotnet", "Alpha.Travel.WebApi.Host.dll"]