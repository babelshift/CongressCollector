#!/bin/bash

# Remove existing JSON files
rm -rf BILLSTATUS/*

# Collect BILLSTATUS from GPO FDSys
dotnet CongressCollector.dll collect billstatus

COMBINEDFILE="output/out.json"

# Combine JSON to a single file for import
sh ./combine-json-files.sh

# Drop existing collection/indexes
mongo < drop-collections.js
mongo < drop-indexes.js

# Import JSON to MongoDB
mongoimport --db test -c bills --file "$COMBINEDFILE" --jsonArray

# Convert data types and create indexes
mongo < strings-to-dates.js
mongo < create-indexes.js
