db.bills.createIndex(
    { CreateDate: 1 },
    { name: "idx_CreateDate" }
);

db.bills.createIndex(
    { UpdateDate: 1 },
    { name: "idx_UpdateDate" }
);

db.bills.createIndex(
    { Congress: 1 },
    { name: "idx_Congress" }
);
