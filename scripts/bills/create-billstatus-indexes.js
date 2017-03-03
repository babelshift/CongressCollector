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
