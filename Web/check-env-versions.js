const semver = require('semver');
const exec = require('child_process').exec;
const pkg = require('./package.json');

const nodeVersion = pkg.engines.node;
const npmVersion = pkg.engines.npm;

if (!semver.satisfies(process.version, nodeVersion)) {
    console.error('Recommended node version ' + nodeVersion + ' not satisfied with current version ' + process.version);
}

exec('npm -v', function (error, stdout, stderr) {
    if (!semver.satisfies(stdout, npmVersion)) {
        console.error('Recommended npm version ' + npmVersion + ' not satisfied with current version ' + stdout);
    }
});