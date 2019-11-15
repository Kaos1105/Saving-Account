create database SavingAccount
go

use SavingAccount
go

--SoTietKiem
--LoaiTietKiem
--ThamSo
--PhieuGuiTien
--PhieuRutTien

create table SoTietKiem
(
MaSoTietKiem int identity primary key, --mã sổ tiết kiệm
TenKhachHang nvarchar(100) not null, --tên khách hàng
SoCMND nvarchar(50) not null, --Số CMND
DiaChi nvarchar(100) not null, --Địa chỉ khách hàng
NgayMoSo date not null, --Ngày mở sổ
SoDu bigint not null default 0, --Số dư của sổ tiết kiệm
MaLoaiTietKiem int not null, --mã loại tiết kiệm
--LaiSuat float, --lãi xuất (theo tháng hoặc kỳ hạn)
DongSo bit not null, --Trạng thái của số tiết kiếm 0: đóng sổ, 1: còn hiệu lực
NgayDaoHan date,
NgayRutTien date not null,
NgayGoiThem date not null,
foreign key (MaLoaiTietKiem) references LoaiTietKiem(MaLoaiTietKiem)
)
go

drop table SoTietKiem
drop table PhieuGuiTien
drop table PhieuRutTien
create table LoaiTietKiem
(
MaLoaiTietKiem int identity primary key not null, --mã loại tiết kiệm
TenLoaiTietKiem nvarchar(100) not null, --tên loại tiết kiệm: không kỳ hạn, 3 tháng, 6 tháng
SoNgayDuocRut int not null default 0, --số ngày kể từ từ khi mở sổ được rút tiền 
DangDung bit not null, --trạng thái của loại tiết kiệm 0: không dùng, 1: được dùng
SoTienGuiToiThieu int not null default 0, --số tiền gửi tối thiểu khi mở sổ
DuocGuiThem bit not null, --sổ có được gửi thêm tiền không
TienGuiThemToiThieu int, -- số tiền gửi thêm tối thiểu vào sổ
LaiSuatThang float, --lãi suất không kỳ hạn
KyHan int, --kỳ hạn tính bằng ngày
LaiSuat float --lãi suất có kỳ hạn
)
go
alter table LoaiTietKiem
add constraint Check_CoKiHan check (KyHan <> 0  and LaiSuat is not null and TienGuiThemToiThieu is null and LaiSuatThang is null)

alter table LoaiTietKiem
add constraint Check_KoKiHan check (KyHan =0 and LaiSuat is null and TienGuiThemToiThieu is not null and LaiSuatThang is not null)

alter table LoaiTietKiem drop column CoKyHan

alter table LoaiTietKiem
drop constraint Check_CoKiHan

alter table LoaiTietKiem
drop column DuocGuiThem

alter table LoaiTietKiem
add ThoiGianDuocGoiThem int

alter table LoaiTietKiem
alter column KyHan int not null 


alter table LoaiTietKiem
add default 0 for KyHan


create table PhieuGuiTien
(
MaPhieuGuiTien int identity primary key not null, --mã phiếu
MaSoTietKiem int not null, --mã sổ tiết kiệm
TenKhachHang nvarchar(100) not null,--tên khách hàng (bảng sổ tiết kiệm)
SoTienGui bigint not null default 0, --số tiền gửi
--LaiSuat float not null, --lãi xuất (theo tháng hoặc kỳ hạn)
NgayGui date not null,--ngày rút
foreign key (MaSoTietKiem) references SoTietKiem(MaSoTietKiem)
)
go

create table PhieuRutTien
(
MaPhieuRutTien int identity primary key not null, --mã phiếu
MaSoTietKiem int not null, --mã sổ tiết kiệm
TenKhachHang nvarchar(100) not null, --tên khách hàng(bảng sổ tiết kiệm)
SoTienRut bigint not null, --số tiền rút
NgayRut date not null,--ngày rút
foreign key (MaSoTietKiem) references SoTietKiem(MaSoTietKiem)
)
go

--*WARNING*
--Do not Execute below query
insert into LoaiTietKiem
(TenLoaiTietKiem, KyHan, DangDung, SoNgayDuocRut, SoTienGuiToiThieu, TienGuiThemToiThieu, LaiSuat, LaiSuatThang, ThoiGianDuocGoiThem)
values 
(N'Kỳ hạn 3 tháng', 90, 1, 30, 1000000,100000,  0.5, null, 90)
insert into LoaiTietKiem
(TenLoaiTietKiem, KyHan, DangDung, SoNgayDuocRut, SoTienGuiToiThieu, TienGuiThemToiThieu, LaiSuat, LaiSuatThang, ThoiGianDuocGoiThem)
values 
(N'Kỳ hạn 6 tháng', 180, 1, 60, 1000000,100000,  0.5, null, 180)
insert into LoaiTietKiem
(TenLoaiTietKiem, KyHan, DangDung, SoNgayDuocRut, SoTienGuiToiThieu, TienGuiThemToiThieu, LaiSuat, LaiSuatThang, ThoiGianDuocGoiThem)
values 
(N'Không kỳ hạn', 0, 1, 15, 1000000,100000,  null, 0.15, 30)
delete LoaiTietKiem
dbcc checkident ('LoaiTietKiem', reseed, 0)
alter table LoaiTietKiem
add constraint Check_CoKiHan check (KyHan <> 0  and LaiSuat is not null and TienGuiThemToiThieu is null and LaiSuatThang is null)

alter table LoaiTietKiem
add constraint Check_KoKiHan check (KyHan =0 and LaiSuat is null and TienGuiThemToiThieu is not null and LaiSuatThang is not null)

--InsertLoaiTietKiem
create proc USP_InsertLoaiTietKiem 
@name nvarchar(100), @interest float, @interestMonth float, @money bigint, @moneyAdd bigint, @dateAdd int, @dateWithdraw int, @isActive bit
as
begin
update LoaiTietKiem set LaiSuat=@interest, LaiSuatThang=@interestMonth, SoTienGuiToiThieu=@money, TienGuiThemToiThieu =@moneyAdd, ThoiGianDuocGoiThem=@dateAdd, SoNgayDuocRut =@dateWithdraw, DangDung=@isActive where TenLoaiTietKiem=@name
end
go 

----AddLoaiTietKiem
alter proc USP_AddLoaiTietKiem 
@name nvarchar(100), @period int, @interest float, @interestMonth float, @money bigint, @moneyAdd bigint, @dateAdd int, @dateWithdraw int, @isActive bit
as
begin
insert into LoaiTietKiem(KyHan, LaiSuat, LaiSuatThang, SoTienGuiToiThieu, TienGuiThemToiThieu, ThoiGianDuocGoiThem, SoNgayDuocRut, DangDung, TenLoaiTietKiem) values (@period, @interest, @interestMonth, @money,  @moneyAdd, @dateAdd,  @dateWithdraw, @isActive ,@name)
end
go 

Delete from LoaiTietKiem where TenLoaiTietKiem = N'Kỳ hạn 3 tháng'

select * from SoTietKiem
select * from LoaiTietKiem
delete from SoTietKiem 
dbcc checkident ('SoTietKiem', reseed, 0)
insert into SoTietKiem(TenKhachHang, SoCMND, DiaChi, NgayMoSo, SoDu, MaLoaiTietKiem, DongSo, NgayDaoHan, NgayRutTien, NgayGoiThem)
values 
(
N'Nguyễn Thành Danh', N'19051999', N'KTX khu B, ĐHQG', GETDATE(), 1000000, 2, 1, DATEADD(DAY, 180, GETDATE()), DATEADD(DAY, 60, GETDATE()) , DATEADD(DAY, 180, GETDATE())
)

insert into SoTietKiem(TenKhachHang, SoCMND, DiaChi, NgayMoSo, SoDu, MaLoaiTietKiem, DongSo, NgayDaoHan, NgayRutTien, NgayGoiThem)
values 
(
N'Hồ Nguyên Bảo', N'17520323', N'KTX khu B, ĐHQG', GETDATE(), 250000, 3, 1, null, DATEADD(DAY, 15, GETDATE()) , DATEADD(DAY, 30, GETDATE())
)
insert into SoTietKiem(TenKhachHang, SoCMND, DiaChi, NgayMoSo, SoDu, MaLoaiTietKiem, DongSo, NgayDaoHan, NgayRutTien, NgayGoiThem)
values 
(
N'Trương Minh Sang', N'17520220', N'KTX khu B, ĐHQG', GETDATE(), 250000, 3, 1, null, DATEADD(DAY, 15, GETDATE()) , DATEADD(DAY, 30, GETDATE())
)

select * from SoTietKiem where MaSoTietKiem = 1
update SoTietKiem set DongSo=1 where MaSoTietKiem =1

--InsertSoTietKiem
alter proc USP_InsertSoTietKiem
@typeID int, @money bigint, @name nvarchar(100), @cmnd nvarchar(100), @address nvarchar(100), @dateOpen date
as
begin
	declare @datePeriod date
	declare @dateAdd date
	declare @dateWithdraw date
	declare @kyHan int
	--select @datePeriod = Dateadd(DAY,  l.Kyhan, @dateOpen ) from LoaiTietKiem l where l.MaLoaiTietKiem=@typeID
	select @kyHan=KyHan from LoaiTietKiem where MaLoaiTietKiem=@typeID
	if(@kyHan=0)
		set @datePeriod = NULL
	else
		set @datePeriod = Dateadd(DAY,  @kyHan, @dateOpen ) 
	select @dateAdd = Dateadd(DAY,  l.ThoiGianDuocGoiThem, @dateOpen ) from LoaiTietKiem l where l.MaLoaiTietKiem=@typeID
	select @dateWithdraw =  Dateadd(DAY,  l.SoNgayDuocRut, @dateOpen ) from LoaiTietKiem l where l.MaLoaiTietKiem=@typeID
	insert into SoTietKiem(TenKhachHang, SoCMND, DiaChi, NgayMoSo, SoDu, MaLoaiTietKiem, DongSo, NgayDaoHan, NgayRutTien, NgayGoiThem)
	values (
	@name, @cmnd, @address, @dateOpen, @money, @typeID, 1, @datePeriod, @dateWithdraw, @dateAdd)
end
go

select * from PhieuGuiTien

alter proc USP_InsertPhieuGuiTien
@accID int, @name nvarchar(100), @money bigint, @dateAdd date
as
begin
	--declare @addMoney int
	--select @addMoney= TienGuiThemToiThieu from LoaiTietKiem l, SoTietKiem stk where l.MaLoaiTietKiem = stk.MaLoaiTietKiem and stk.MaSoTietKiem = @accID
	update SoTietKiem set SoDu = SoDu +@money where @accID = MaSoTietKiem
	insert into PhieuGuiTien(MaSoTietKiem, TenKhachHang, SoTienGui, NgayGui)
	values (@accID, @name, @money, @dateAdd)
end
go

select max(MaPhieuGuiTien) from PhieuGuiTien

select * from LoaiTietKiem L where exists (select 1 from SoTietKiem S where L.MaLoaiTietKiem = S.MaLoaiTietKiem) and L.MaLoaiTietKiem =  15

select * from PhieuRutTien

alter proc USP_InsertPhieuRutTien
@accID int, @name nvarchar(100), @money bigint, @dateWithdraw date, @tatToan bit
as
begin
	update SoTietKiem set SoDu = SoDu -@money where @accID = MaSoTietKiem
	insert into PhieuRutTien(MaSoTietKiem, TenKhachHang, SoTienRut, NgayRut, TatToan)
	values (@accID, @name, @money, @dateWithdraw, @tatToan)
end
go

exec USP_InsertPhieuGuiTien 2, N'Hồ Nguyên Bảo', 500000, '6/22/2019'

select * from (select TenLoaiTietKiem , sum(SoTienGui) as TongThu into TongThuNgay
from PhieuGuiTien pgt, LoaiTietKiem ltk, SoTietKiem stk
where pgt.MaSoTietKiem=stk.MaSoTietKiem and stk.MaLoaiTietKiem = ltk.MaLoaiTietKiem and pgt.NgayGui=GETDATE()
group by TenLoaiTietKiem), (select TenLoaiTietKiem , sum(SoTienRut) as TongChi into TongChiNgay
from PhieuRutTien prt, LoaiTietKiem ltk, SoTietKiem stk
where prt.MaSoTietKiem=stk.MaSoTietKiem and stk.MaLoaiTietKiem = ltk.MaLoaiTietKiem and prt.NgayGui=GETDATE()
group by TenLoaiTietKiem) 


	
select TenLoaiTietKiem , sum(SoTienGui) as TongThu, sum(SoTienRut) as TongChi
from PhieuGuiTien pgt, PhieuRutTien prt, LoaiTietKiem ltk, SoTietKiem stk
where pgt.MaSoTietKiem=stk.MaSoTietKiem and stk.MaLoaiTietKiem = ltk.MaLoaiTietKiem and pgt.NgayGui=GETDATE() and  prt.MaSoTietKiem=stk.MaSoTietKiem and stk.MaLoaiTietKiem = ltk.MaLoaiTietKiem and prt.NgayRut=GETDATE()
group by TenLoaiTietKiem

select * from PhieuGuiTien 
select * from PhieuRutTien

select * from
(select TenLoaiTietKiem as tltk_1, sum(SoTienGui) as TongThu
from PhieuGuiTien pgt, LoaiTietKiem ltk, SoTietKiem stk
where pgt.MaSoTietKiem=stk.MaSoTietKiem and stk.MaLoaiTietKiem = ltk.MaLoaiTietKiem and pgt.NgayGui='6/8/2019'
group by TenLoaiTietKiem) t1
full outer join
(select TenLoaiTietKiem as tltk_2, sum(SoTienRut) as TongChi
from PhieuRutTien prt, LoaiTietKiem ltk, SoTietKiem stk
where prt.MaSoTietKiem=stk.MaSoTietKiem and stk.MaLoaiTietKiem = ltk.MaLoaiTietKiem and prt.NgayRut='6/8/2019'
group by TenLoaiTietKiem) t2
on t1.tltk_1=t2.tltk_2

--select TenLoaiTietKiem , sum(SoTienRut) as TongChi, sum(SoTienGui) as TongThu 
--from PhieuGuiTien pgt, LoaiTietKiem ltk, SoTietKiem stk, PhieuRutTien prt
--where pgt.MaSoTietKiem=stk.MaSoTietKiem and stk.MaLoaiTietKiem = ltk.MaLoaiTietKiem and prt.MaSoTietKiem=stk.MaSoTietKiem
--group by TenLoaiTietKiem

delete from PhieuGuiTien
dbcc checkident ('PhieuGuiTien', reseed, 0)

delete from PhieuRutTien
dbcc checkident ('PhieuRutTien', reseed, 0)

--alter proc USP_ReportDate
--@date date
--as
--begin
--	select t1.TenLoaiTietKiem, t1.TongThu, t2.TongChi from	
--	(select TenLoaiTietKiem, sum(SoTienGui) as TongThu
--	from PhieuGuiTien pgt, LoaiTietKiem ltk, SoTietKiem stk
--	where pgt.MaSoTietKiem=stk.MaSoTietKiem and stk.MaLoaiTietKiem = ltk.MaLoaiTietKiem and pgt.NgayGui=@date
--	group by TenLoaiTietKiem) t1
--	join
--	(select TenLoaiTietKiem, sum(SoTienRut) as TongChi
--	from PhieuRutTien prt, LoaiTietKiem ltk, SoTietKiem stk
--	where prt.MaSoTietKiem=stk.MaSoTietKiem and stk.MaLoaiTietKiem = ltk.MaLoaiTietKiem and prt.NgayRut=@date
--	group by TenLoaiTietKiem) t2
--	on t1.TenLoaiTietKiem = t2.TenLoaiTietKiem
--end
--go
exec USP_ReportDate @date='6/5/2019'
select t1.TenLoaiTietKiem, t1.TongThu, t2.TongChi from (select TenLoaiTietKiem, sum(SoTienGui) as TongThu from PhieuGuiTien pgt, LoaiTietKiem ltk, SoTietKiem stk where pgt.MaSoTietKiem = stk.MaSoTietKiem and stk.MaLoaiTietKiem = ltk.MaLoaiTietKiem and pgt.NgayGui = '06/05/2019' group by TenLoaiTietKiem) t1 join (select TenLoaiTietKiem, sum(SoTienRut) as TongChi from PhieuRutTien prt, LoaiTietKiem ltk, SoTietKiem stk where prt.MaSoTietKiem = stk.MaSoTietKiem and stk.MaLoaiTietKiem = ltk.MaLoaiTietKiem and prt.NgayRut = '06/05/2019' group by TenLoaiTietKiem) t2 on t1.TenLoaiTietKiem = t2.TenLoaiTietKiem

alter table PhieuRutTien add TatToan bit not null default 0

--select t1.TenLoaiTietKiem, t1.SoMo, t2.SoDong from 
--(select TenLoaiTietKiem, count(stk.MaLoaiTietKiem) as SoMo from SoTietKiem stk, LoaiTietKiem ltk where Month(stk.NgayMoSo)=6 and stk.MaLoaiTietKiem=ltk.MaLoaiTietKiem group by TenLoaiTietKiem) t1
--join (select TenLoaiTietKiem, count(MaPhieuRutTien) as SoDong from PhieuRutTien prt, SoTietKiem stk, LoaiTietKiem ltk where Month(NgayRut)=6 and TatToan=1 and prt.MaSoTietKiem=stk.MaSoTietKiem and stk.MaLoaiTietKiem = ltk.MaLoaiTietKiem group by TenLoaiTietKiem) t2 on t1.TenLoaiTietKiem =t2.TenLoaiTietKiem

select * from 
(select stk.NgayMoSo, count(stk.MaLoaiTietKiem) as SoMo from SoTietKiem stk, LoaiTietKiem ltk where MONTH(stk.NgayMoSo)= 6 and ltk.TenLoaiTietKiem=N'Kỳ hạn 6 tháng' and stk.MaLoaiTietKiem = ltk.MaLoaiTietKiem group by stk.NgayMoSo) t1
full outer join
(select prt.NgayRut, count(prt.MaPhieuRutTien) as SoDong from PhieuRutTien prt, LoaiTietKiem ltk, SoTietKiem stk where MONTH(prt.NgayRut)= 6 and ltk.TenLoaiTietKiem=N'Kỳ hạn 6 tháng' and stk.MaLoaiTietKiem = ltk.MaLoaiTietKiem and stk.MaSoTietKiem=prt.MaSoTietKiem group by prt.NgayRut) t2
on t1.NgayMoSo = t2.NgayRut order by NgayRut asc, NgayMoSo asc

select * from (select stk.NgayMoSo, count(stk.MaLoaiTietKiem) as SoMo from SoTietKiem stk, LoaiTietKiem ltk where MONTH(stk.NgayMoSo) = 6 and ltk.TenLoaiTietKiem = N'Kỳ hạn 6 tháng' and stk.MaLoaiTietKiem = ltk.MaLoaiTietKiem group by stk.NgayMoSo) t1 full outer join (select prt.NgayRut, count(prt.MaPhieuRutTien) as SoDong from PhieuRutTien prt, LoaiTietKiem ltk, SoTietKiem stk where MONTH(prt.NgayRut) =6 and ltk.TenLoaiTietKiem = N'Kỳ hạn 6 tháng' and stk.MaLoaiTietKiem = ltk.MaLoaiTietKiem and stk.MaSoTietKiem = prt.MaSoTietKiem group by prt.NgayRut) t2 on t1.NgayMoSo = t2.NgayRut order by NgayRut asc, NgayMoSo asc
select * from LoaiTietKiem
select * from SoTietKiem

update LoaiTietKiem set ThoiGianDuocGoiThem= 180 where MaLoaiTietKiem =1
select LaiSuatThang from LoaiTietKiem where MaLoaiTietKiem =3
