#!/bin/bash

# Remove existing JSON files
echo "=== REMOVING EXISTING BILLSTATUS ==="
rm -rf BILLSTATUS/*

# Collect BILLSTATUS from GPO FDSys
echo "=== GETTING NEW BILLSTATUS ==="
dotnet CongressCollector.dll collect billstatus

COMBINEDFILE="output/out.json"
USER=""
PWD=""
DB=""

# Combine JSON to a single file for import
echo "=== COMBINING TO SINGLE JSON FILE ==="
sh ./combine-json-files.sh

# Drop existing collection/indexes
echo "=== DROPPING EXISTING DATA FROM MONGODB ==="
mongo -u "$USER" -p "$PWD" --authenticationDatabase "$DB" < drop-indexes.js
mongo -u "$USER" -p "$PWD" --authenticationDatabase "$DB" < drop-collections.js

# Import JSON to MongoDB
echo "=== IMPORTING NEW DATA DATA TO MONGODB ==="
mongoimport --username "$USER" --password "$PWD" --db "$DB" -c bills --file "$COMBINEDFILE" --jsonArray

# Convert data types and create indexes
echo "=== CONVERTING STRINGS TO DATES AND CREATING INDEXES ==="
mongo -u "$USER" -p "$PWD" --authenticationDatabase "$DB" < strings-to-dates.js
mongo -u "$USER" -p "$PWD" --authenticationDatabase "$DB" < create-indexes.js
