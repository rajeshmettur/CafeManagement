Cafe Management
cd CafeManagement


--Setup Database
run docker-compose file to install SQL
dotnet ef migrations add initialCommit -s API -p Persistence
dotnet ef database update -s API -p Persistence
cd API

--Configure the baseUrl API EndPoint

--Setup Client
cd client-app
npm install
npm start
