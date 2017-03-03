#!/bin/bash

# Remove existing JSON files
echo "=== REMOVING EXISTING DATA ==="
rm -rf bin/BILLS/*

# Collect BILLSTATUS from GPO FDSys
echo "=== GETTING NEW BILLS TEXT ==="
dotnet bin/CongressCollector.dll collect bills

BILLSTEXTFILE="output/billstext.json"
USER=""
PWD=""
DB=""

# Combine JSON to a single file for import
echo "=== COMBINING BILLS TEXT TO SINGLE JSON FILE ==="
sh ./combine-json-billstext.sh

# Drop existing collection/indexes
echo "=== DROPPING EXISTING INDEXES AND DATA FROM MONGODB ==="
mongo -u "$USER" -p "$PWD" --authenticationDatabase "$DB" < drop-billstext-collection.js

# Import JSON to MongoDB
echo "=== IMPORTING NEW BILLS TEXT TO MONGODB ==="
mongoimport --username "$USER" --password "$PWD" --db "$DB" -c billstext --file "$BILLSTEXTFILE" --jsonArray
