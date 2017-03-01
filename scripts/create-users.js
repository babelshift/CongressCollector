use usgov;
db.createUser({
	user: "",
	pwd: "",
	roles: [ 
		{ role: "dbAdmin", db: "usgov" },
		{ role: "readWrite", db: "usgov" }
	]
});
