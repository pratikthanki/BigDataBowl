FROM microsoft/aspnetcore-build
EXPOSE 4321
WORKDIR /app
COPY . .
RUN dotnet restore
ENTRYPOINT ["dotnet", "run"]