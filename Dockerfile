FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

#Copy published only
#COPY bin/Release/netcoreapp2.1/publish/ /app/out

# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .

# Install krb5 packages
RUN apt-get update
RUN apt-get remove krb5-config krb5-user
RUN apt install -y krb5-config 
RUN apt-get install -y krb5-user

# Copy kerberso configuration and keytab
COPY krb5.conf /etc/krb5.conf
COPY myUserName.keytab /app/myUserName.keytab

# copy the launch script
COPY launch.sh /launch.sh

ENTRYPOINT /launch.sh