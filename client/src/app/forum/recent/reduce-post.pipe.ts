import { Pipe, PipeTransform, SecurityContext } from "@angular/core";
import { DomSanitizer, SafeHtml } from "@angular/platform-browser";
import { OperationFactory } from "@ckeditor/ckeditor5-engine";

@Pipe({
  standalone: true,
  name: 'reducePost'
})
export class ReducePost implements PipeTransform {

  // skip some tags, that just wrap text
  private ignorTags = ['span', 'oembed'];

  // limit number of specific tags
  private tagLimmiter = [
    {tag: 'figure', limit: 2},
  ]

  constructor(private sanitizer: DomSanitizer) {}

  transform(value: string, symbolLimit: number, tagLimit: number) : SafeHtml {
    let result = '';
    let symbolCount = 0;
    let tagCount = 0;
    let insideTag = false;

    let openIndex = 0;

    //count open and close tags
    let tagCounter: {tag: string, count: number}[] = [];

    let tagName = '';
    let tagNameReaded = false;

    for (let i = 0; i < value.length; i++) {
      const char = value[i];

      if (char === '<') {
        insideTag = true;
        openIndex = i;

        // not count closing tags
        if(value[i+1] !== '/')
          tagCount++;

      } else if (char === '>') {
        insideTag = false;

        // check if tag is ignored
        if(this.ignorTags.indexOf(tagName) !== -1) {
          tagCount--;
        }

        // reduce tag name if is closing tag
        if(tagName[0] === '/') {
          tagName = tagName.substring(1);
        }

        // add to tag counter
        let tagIndex = tagCounter.findIndex(x => x.tag == tagName);
        if(tagIndex === -1) {
          tagCounter.push({tag: tagName, count: 1});
          tagIndex = tagCounter.length - 1;
        }
        else {
          tagCounter[tagIndex].count++;
        }

        // skip if limitted
        let tagLimitIndex = this.tagLimmiter.findIndex(x => x.tag == tagName);
        if(tagLimitIndex !== -1) {
          let closeTag = `</${tagName}>`
          console.log(tagName, tagCounter[tagIndex].count)


          if(tagCounter[tagIndex].count > this.tagLimmiter[tagLimitIndex].limit) {
            // remove last tag and upd index
            result = result.substring(0, openIndex);
            i = value.indexOf(closeTag, openIndex) + closeTag.length - 1;

            tagCount--;
            tagNameReaded = false;
            tagName = '';
            continue;
          }
        }

        // reset tag name values
        tagNameReaded = false;
        tagName = '';
      }

      if (!insideTag) {
        symbolCount++;
      }
      else {
        // read tag name
        if(char !== '<')
        if (char === ' ' || char === '\n') {
          tagNameReaded = true;
        }
        else if (!tagNameReaded) {
          tagName += char;
        }
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

    //console.log(value, result, tagCounter)

    return result;
  }

}
