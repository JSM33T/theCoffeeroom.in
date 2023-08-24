const fs = require('fs');

const configPath = 'replacements.json';
const inputFile = process.argv[2];
const outputFile = process.argv[3];

if (!inputFile || !outputFile) {
    console.error("Usage: node replaceAndDelete.js <input-file> <output-file>");
    process.exit(1);
}

try {
    const configContent = fs.readFileSync(configPath, 'utf8');
    const config = JSON.parse(configContent);
    const inputContent = fs.readFileSync(inputFile, 'utf8');
    const contentWithoutComments = inputContent.replace(/<!--(.*?)-->/gs, '');

    // Apply replacements
    let modifiedContent = contentWithoutComments;
    config.replacements.forEach(replacement => {
        const find = new RegExp(replacement.find, 'g');
        const replace = replacement.replace.replace(/\\n/g, '\n'); // Convert \\n to actual newline
        modifiedContent = modifiedContent.replace(find, replace);
    });

    //space reducer
    while (modifiedContent.includes('  ')) {
        modifiedContent = modifiedContent.replace(/ {2}/g, ' ');
    }
    fs.writeFileSync(outputFile, modifiedContent);
    console.log(`Commented sections removed, replacements applied, and double spaces replaced in ${inputFile} saved to ${outputFile}`);
} catch (error) {
    console.error('An error occurred:', error);
}
