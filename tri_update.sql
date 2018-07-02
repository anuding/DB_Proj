use db_Market
go
create trigger tri_update on TB_ShoppingRecord for insert
as
declare @SRid nchar(10)
declare @Cid nchar(10)
declare @SRturnover float(24)

select @SRid=SRid,@SRturnover=SRturnover,@Cid=Cid from inserted
update TB_Customer set Cdeposit=Cdeposit-@SRturnover where Cid=@Cid
