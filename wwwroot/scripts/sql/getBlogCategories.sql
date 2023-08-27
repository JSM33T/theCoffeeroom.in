SELECT
    BC.Id AS CategoryId,
    BC.Title AS CategoryTitle,
    BC.Locator AS CategoryLocator,
    COUNT(BM.Id) AS NumberOfItems
FROM 
    coffeeroomdb.dbo.TblBlogCategory BC
LEFT JOIN 
    coffeeroomdb.dbo.TblBlogMaster BM ON BC.Id = BM.CategoryId AND BM.IsActive = 1
GROUP BY 
    BC.Id, BC.Title, BC.Locator
