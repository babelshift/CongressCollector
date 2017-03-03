use usgov;
db.billstatuses.createIndex(
    { CreateDate: 1 },
    { name: "idx_CreateDate" }
);

db.billstatuses.createIndex(
    { UpdateDate: 1 },
    { name: "idx_UpdateDate" }
);

db.billstatuses.createIndex(
    { Congress: 1 },
    { name: "idx_Congress" }
);

db.billstatuses.createIndex(
    { IntroducedDate: 1 },
    { name: "idx_IntroducedDate" }
);

db.committees.createIndex(
    { "thomas_id": 1 },
    { name: "idx_ThomasId" }
);

db.legislators.createIndex(
    { "id.bioguide": 1 },
    { name: "idx_BioguideId" }
);
