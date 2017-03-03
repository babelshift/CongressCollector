#!/bin/bash

YAMLCONVERTARGS='import sys, yaml, json; json.dump(yaml.load(sys.stdin), sys.stdout, indent=4)'
APPSDIR=~/Documents/apps/CongressLegislators/
SRCDIR=~/Documents/src/Congress/congress-legislators/
YAML1="committee-membership-current"
YAML2="committees-current"
YAML3="legislators-current"
YAML4="legislators-social-media"

# remove existing yaml/json
echo "=== REMOVING EXISTING YAML/JSON ==="
rm -f yaml/*
rm -f json/*

# git pull from congress-legislators repo
echo "=== GETTING LATEST YAML FROM GITHUB ==="
cd $SRCDIR
git pull

# copy yml files to apps area
cp *.yaml $APPSDIR/yaml

# convert yml files to json files
cd $APPSDIR
echo "=== CONVERTING $YAML1 TO JSON"
python3 -c "$YAMLCONVERTARGS" < yaml/$YAML1.yaml > json/$YAML1.json
echo "=== CONVERTING $YAML2 TO JSON"
python3 -c "$YAMLCONVERTARGS" < yaml/$YAML2.yaml > json/$YAML2.json
echo "=== CONVERTING $YAML3 TO JSON"
python3 -c "$YAMLCONVERTARGS" < yaml/$YAML3.yaml > json/$YAML3.json
echo "=== CONVERTING $YAML4 TO JSON"
python3 -c "$YAMLCONVERTARGS" < yaml/$YAML4.yaml > json/$YAML4.json

USER=""
PWD=""
DB=""

# drop existing data in mongodb
echo "=== DROPPING EXISTING LEGISLATORS ==="
mongo -u "$USER" -p "$PWD" --authenticationDatabase "$DB" < drop-collections.js

# import json files to mongodb
echo "=== IMPORTING $YAML1.json TO MONGODB ==="
mongoimport --username "$USER" --password "$PWD" --db "$DB" -c "memberships" --file "json/$YAML1.json"
echo "=== IMPORTING $YAML2.json TO MONGODB ==="
mongoimport --username "$USER" --password "$PWD" --db "$DB" -c "committees" --file "json/$YAML2.json" --jsonArray
echo "=== IMPORTING $YAML3.json TO MONGODB ==="
mongoimport --username "$USER" --password "$PWD" --db "$DB" -c "legislators" --file "json/$YAML3.json" --jsonArray
echo "=== IMPORTING $YAML4.json TO MONGODB ==="
mongoimport --username "$USER" --password "$PWD" --db "$DB" -c "socialmedia" --file "json/$YAML4.json" --jsonArray

# create indexes
