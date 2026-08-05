---
Статус: 19
---
```dataviewjs
const currentFile = dv.current().file;

const children = dv.pages()
    .where(p => p.ParentLink?.path === currentFile.path);

// Берём только заметки, где есть числовой Статус
const statuses = children
    .map(p => {
        const value = Number(p["Статус"]);
        return isNaN(value) ? null : value;
    })
    .filter(v => v !== null)
    .array();

const percent = statuses.length > 0
    ? Math.round(statuses.reduce((a, b) => a + b, 0) / statuses.length)
    : 0;

// Обновляем статус текущей заметки
await app.fileManager.processFrontMatter(
    app.vault.getAbstractFileByPath(currentFile.path),
    (frontmatter) => {
        frontmatter["Статус"] = percent;
    }
);

// Вывод дочерних заметок
dv.table(
    ["Название", "Статус"],
    children.map(p => [
        p.file.link,
        p["Статус"] ?? "Нет статуса"
    ])
);
```