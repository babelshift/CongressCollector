use usgov;
db.createUser({
	user: "congress-collector",
	pwd: "s9p9t84",
	roles: [ 
		{ role: "dbAdmin", db: "usgov" },
		{ role: "readWrite", db: "usgov" }
	]
});
