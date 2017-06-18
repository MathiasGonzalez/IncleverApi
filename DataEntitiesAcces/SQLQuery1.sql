select * from Users u inner join GroupPermissions gp on u.userid = gp.userid

select * from GroupPermissions

select * from Groups

select * from Fields

select 
	sn.snipetid	
from Snippets sn inner join Fields f on f.snipettid = sn.snipetid
where  /*f.name like '%we%' OR */ f.value like '%b%'
group by sn.snipetid

delete Snippets