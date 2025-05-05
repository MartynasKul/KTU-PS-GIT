  public static async cleanup(): Promise<void> {
    return await database.write(async () => {
      return await database
        .get<Series>(TableName.WATCH_HISTORY)
        .query()
        .destroyAllPermanently();
    });
  }

  counts.map((c) => {
    const cat = categories.find((cat) => cat.category.id === c.category.id);
    if (cat) {
      cat.count = c.count;
      categoriesWithUpdatedCount.push(cat);
    }
  });
  const index = categories.findIndex((c) => !c.category.isCustom);
  const sliceIndex = index === -1 ? categories.length : index; ???????????????????/
  return [...categoriesWithUpdatedCount, ...categories.slice(sliceIndex)];
