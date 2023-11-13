/**
 * @license Copyright (c) 2014-2023, CKSource Holding sp. z o.o. All rights reserved.
 * For licensing, see LICENSE.md or https://ckeditor.com/legal/ckeditor-oss-license
 */

import { ClassicEditor } from '@ckeditor/ckeditor5-editor-classic';

import { Alignment } from '@ckeditor/ckeditor5-alignment';
import { Autoformat } from '@ckeditor/ckeditor5-autoformat';
import { Bold, Italic, Underline } from '@ckeditor/ckeditor5-basic-styles';
import { BlockQuote } from '@ckeditor/ckeditor5-block-quote';
import { CodeBlock } from '@ckeditor/ckeditor5-code-block';
import { Essentials } from '@ckeditor/ckeditor5-essentials';
import { FontBackgroundColor, FontColor, FontFamily, FontSize } from '@ckeditor/ckeditor5-font';
import { Highlight } from '@ckeditor/ckeditor5-highlight';
import {
	AutoImage,
	Image,
	ImageCaption,
	ImageResize,
	ImageStyle,
	ImageToolbar
} from '@ckeditor/ckeditor5-image';
import { Indent, IndentBlock } from '@ckeditor/ckeditor5-indent';
import { AutoLink, Link } from '@ckeditor/ckeditor5-link';
import { List } from '@ckeditor/ckeditor5-list';
import { MediaEmbed } from '@ckeditor/ckeditor5-media-embed';
import { Paragraph } from '@ckeditor/ckeditor5-paragraph';
import { RemoveFormat } from '@ckeditor/ckeditor5-remove-format';
import { SelectAll } from '@ckeditor/ckeditor5-select-all';
import {
	Table,
	TableCellProperties,
	TableColumnResize,
	TableProperties,
	TableToolbar
} from '@ckeditor/ckeditor5-table';

// You can read more about extending the build with additional plugins in the "Installing plugins" guide.
// See https://ckeditor.com/docs/ckeditor5/latest/installation/plugins/installing-plugins.html for details.

class Editor extends ClassicEditor {
	public static override builtinPlugins = [
		Alignment,
		AutoImage,
		AutoLink,
		Autoformat,
		BlockQuote,
		Bold,
		CodeBlock,
		Essentials,
		FontBackgroundColor,
		FontColor,
		FontFamily,
		FontSize,
		Highlight,
		Image,
		ImageCaption,
		ImageResize,
		ImageStyle,
		ImageToolbar,
		Indent,
		IndentBlock,
		Italic,
		Link,
		List,
		MediaEmbed,
		Paragraph,
		RemoveFormat,
		SelectAll,
		Table,
		TableCellProperties,
		TableColumnResize,
		TableProperties,
		TableToolbar,
		Underline
	];

	public static override defaultConfig = {
		toolbar: {
			items: [
				'bold',
				'italic',
				'underline',
				'highlight',
				'|',
				'fontSize',
				'fontFamily',
				'fontColor',
				'fontBackgroundColor',
				'alignment',
				'|',
				'bulletedList',
				'numberedList',
				'|',
				'removeFormat',
				'-',
				'link',
				'codeBlock',
				'blockQuote',
				'mediaEmbed',
				'insertTable'
			],
			shouldNotGroupWhenFull: true
		},
		language: 'en',
		image: {
			toolbar: [
				'imageTextAlternative',
				'toggleImageCaption',
				'imageStyle:inline',
				'imageStyle:block',
				'imageStyle:side'
			]
		},
		table: {
			contentToolbar: [
				'tableColumn',
				'tableRow',
				'mergeTableCells',
				'tableCellProperties',
				'tableProperties'
			]
		},
    mediaEmbed: {
      // git with default providers config:
      // https://github.com/ckeditor/ckeditor5/blob/master/packages/ckeditor5-media-embed/src/mediaembedediting.ts
      // cd .\ckeditor5\ && npm run build && cd .. && npm install ./ckeditor5/ && ng serve
      providers: [
        {
          name: "youtube-nocookie",
          url: [
						/^(?:m\.)?youtube\.com\/watch\?v=([\w-]+)(?:&t=(\d+))?/,
						/^(?:m\.)?youtube\.com\/v\/([\w-]+)(?:\?t=(\d+))?/,
						/^youtube\.com\/embed\/([\w-]+)(?:\?start=(\d+))?/,
						/^youtu\.be\/([\w-]+)(?:\?t=(\d+))?/
					],
          html: (match:any) => {
            const id = match[ 1 ];
            const time = match[ 2 ];

            return (
              '<div>' +
                `<iframe src="https://www.youtube-nocookie.com/embed/${ id }${ time ? `?start=${ time }` : '' }" ` +
                  'frameborder="0" allow="autoplay; encrypted-media" allowfullscreen>' +
                '</iframe>' +
              '</div>'
            );
          }
        },
      ],
    }
	};
}

export default Editor;
