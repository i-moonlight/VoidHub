import { Pipe, PipeTransform, SecurityContext } from "@angular/core";
import { DomSanitizer, SafeHtml } from "@angular/platform-browser";

@Pipe({
  standalone: true,
  name: 'reducePost'
})
export class ReducePost implements PipeTransform {

  constructor(private sanitizer: DomSanitizer) {}

  transform(value: any, symbolLimit: number, tagLimit: number) : SafeHtml {
    let result = '';
    let symbolCount = 0;
    let tagCount = 0;
    let insideTag = false;

    for (let i = 0; i < value.length; i++) {
      const char = value[i];

      if (char === '<') {
        insideTag = true;
        tagCount++;

      } else if (char === '>') {
        insideTag = false;
      }

      if (!insideTag) {
        symbolCount++;
      }

      result += char;

      if (symbolCount >= symbolLimit || tagCount > tagLimit) {
        break;
      }
    }

    // Remove the remaing brace if tag limit reached out
    if (tagCount > tagLimit) {
      result = result.substring(0, result.length - 1);
    }

    return result;
  }

}
