#!/bin/bash

# create the output directory (if it does not exist)
mkdir -p output

# remove result from previous runs
rm output/billstatuses.json

# add first opening bracked
echo [ >> output/tmp.json
# use all json files in current folder
for i in bin/BILLSTATUS/*/*/*.json
do 
    # dump the file's content
    cat "$i" >> output/tmp.json
    # add a comma afterwards
    echo , >>  output/tmp.json
done
# remove the last comma from the file; otherwise it's not valid json
cat output/tmp.json | sed '$ s/.$//' >> output/billstatuses.json
# remove tempfile
rm output/tmp.json
# add closing bracket
echo ] >> output/billstatuses.json
