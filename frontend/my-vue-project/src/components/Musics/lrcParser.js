// lrcParser.js
export default function parseLrc(lrcContent) {
    const lines = lrcContent.split('\n');
    const parsedLyrics = [];
  
    lines.forEach(line => {
      const match = line.match(/\[(\d{2}):(\d{2})\.(\d{2})\](.*)/);
      if (match) {
        const time = `${match[1]}:${match[2]}.${match[3]}`;
        const content = match[4].trim();
        parsedLyrics.push({ time, content });
      } else {
        // 处理没有时间戳的普通文本，例如歌曲标题或艺术家信息
        // 这里可以选择忽略它们，或者存储在一个单独的数组中
      }
    });
  
    return parsedLyrics.sort((a, b) => a.time.localeCompare(b.time));
  }