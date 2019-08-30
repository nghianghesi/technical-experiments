Test mongo db query: string in list of string, case-insensitive
- can't use $in & $regex
- $regex case-insensitive doesn't optimized with indexing
- $text search is good, but got problem with OR whole match, also $text is not intended for this purpose.
- => final solution: add additional indexed lower field