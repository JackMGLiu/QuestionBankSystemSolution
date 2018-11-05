CREATE TABLE [dbo].[DictType](
	[DictId] [nvarchar](50) NOT NULL,
	[ParentId] [nvarchar](50) NULL,
	[DictCode] [nvarchar](50) NULL,
	[DictName] [nvarchar](50) NULL,
	[IsTree] [int] NULL,
	[IsNav] [int] NULL,
	[SortCode] [int] NULL,
	[DeleteMark] [int] NULL,
	[EnabledMark] [int] NULL,
	[Remark] [nvarchar](200) NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserId] [nvarchar](50) NULL,
	[CreateUserName] [nvarchar](50) NULL,
	[ModifyTime] [datetime] NULL,
	[ModifyUserId] [nvarchar](50) NULL,
	[ModifyUserName] [nvarchar](50) NULL,
 CONSTRAINT [PK_DATATYPE] PRIMARY KEY NONCLUSTERED 
(
	[DictId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



CREATE TABLE [dbo].[DictItem](
	[DictItemId] [nvarchar](50) NOT NULL,
	[DictId] [nvarchar](50) NULL,
	[ParentId] [nvarchar](50) NULL,
	[ItemCode] [nvarchar](50) NULL,
	[ItemName] [nvarchar](50) NULL,
	[ItemValue] [nvarchar](50) NULL,
	[QuickQuery] [nvarchar](200) NULL,
	[SimpleSpelling] [nvarchar](200) NULL,
	[IsDefault] [int] NULL,
	[SortCode] [int] NULL,
	[DeleteMark] [int] NULL,
	[EnabledMark] [int] NULL,
	[Remark] [nvarchar](200) NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserId] [nvarchar](50) NULL,
	[CreateUserName] [nvarchar](50) NULL,
	[ModifyTime] [datetime] NULL,
	[ModifyUserId] [nvarchar](50) NULL,
	[ModifyUserName] [nvarchar](50) NULL,
 CONSTRAINT [PK_DATAITEM] PRIMARY KEY NONCLUSTERED 
(
	[DictItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO