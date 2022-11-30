
let editor;


ClassicEditor
    .create(document.querySelector('#editor'), {
        toolbar: {
            items: [
                'heading',
                '|',
                'alignment',                                                
                'bold',
                'italic',
                'link',
                'bulletedList',
                'numberedList',
                'blockQuote',
                'undo',
                'redo'
            ],

        },
        //}
    })
    .then(newEditor => {
        editor = newEditor;
        
    })
    .catch(error => {
    });

