use usgov;
db.billstatuses.find().forEach(function(doc) {
    
    doc.CreateDate = new ISODate(doc.CreateDate);

    doc.UpdateDate = new ISODate(doc.UpdateDate);

    doc.IntroducedDate = new ISODate(doc.IntroducedDate);

    doc.Actions.forEach(function(action) {
        action.ActionDate = new ISODate(action.ActionDate);
    });

    doc.Committees.forEach(function(committee) {
        committee.Activities.forEach(function(activity) {
            activity.Date = new ISODate(activity.Date);
        });
        committee.Subcommittees.forEach(function(subcommittee) {
            subcommittee.Activities.forEach(function(activity) {
                activity.Date = new ISODate(activity.Date);
            });
        });
    });
    
    doc.RelatedBills.forEach(function(relatedBill) {
        relatedBill.LatestAction.ActionDate = new ISODate(relatedBill.LatestAction.ActionDate);
    });
    
    doc.Summaries.forEach(function(summary) {
        summary.ActionDate = new ISODate(summary.ActionDate);
        summary.UpdateDate = new ISODate(summary.UpdateDate);
    });

    db.billstatuses.save(doc);
});
