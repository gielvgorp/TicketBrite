# Gebruik de officiële SQL Server image als basis
FROM mcr.microsoft.com/mssql/server:2019-latest

# Voeg je eigen database-bestanden of configuraties toe (indien nodig)
# Bijvoorbeeld: kopieer een custom configuratiebestand of een init script
# COPY ./init.sql /init.sql

# Geef de omgevingseigen variabelen voor de database op
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=YourStrong!Passw0rd

# Stel de poort in voor de database
EXPOSE 1433
