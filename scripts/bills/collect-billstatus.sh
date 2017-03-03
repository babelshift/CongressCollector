#!/bin/bash

# Remove existing JSON files
echo "=== REMOVING EXISTING DATA ==="
rm -rf bin/BILLSTATUS/*

# Collect BILLSTATUS from GPO FDSys
echo "=== GETTING NEW BILLSTATUS ==="
dotnet bin/CongressCollector.dll collect billstatus

BILLSTATUSESFILE="output/billstatuses.json"
USER=""
PWD=""
DB=""

# Combine JSON to a single file for import
echo "=== COMBINING BILL STATUSES TO SINGLE JSON FILE ==="
sh ./combine-json-billstatuses.sh

# Drop existing collection/indexes
echo "=== DROPPING EXISTING INDEXES AND DATA FROM MONGODB ==="
mongo -u "$USER" -p "$PWD" --authenticationDatabase "$DB" < drop-billstatus-indexes.js
mongo -u "$USER" -p "$PWD" --authenticationDatabase "$DB" < drop-billstatus-collection.js

# Import JSON to MongoDB
echo "=== IMPORTING NEW BILL STATUSES TO MONGODB ==="
mongoimport --username "$USER" --password "$PWD" --db "$DB" -c billstatuses --file "$BILLSTATUSESFILE" --jsonArray

# Convert data types and create indexes
echo "=== CONVERTING STRINGS TO DATES AND CREATING INDEXES ==="
mongo -u "$USER" -p "$PWD" --authenticationDatabase "$DB" < strings-to-dates.js
mongo -u "$USER" -p "$PWD" --authenticationDatabase "$DB" < create-billstatus-indexes.js
