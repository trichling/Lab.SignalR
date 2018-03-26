const path = require('path');
const webpack = require('webpack');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const CheckerPlugin = require('awesome-typescript-loader').CheckerPlugin;
const bundleOutputDir = './wwwroot/views';

module.exports = {
    
        entry: { 
            'Chat/Index': './Views/Chat/index.ts'
        },
         output: {
            path: path.join(__dirname, bundleOutputDir),
            filename: '[name].js',
            publicPath: '/views/'
        },
        resolve: {
            extensions: ['.js', '.ts', '.html'],
            alias: {
                'vue$': 'vue/dist/vue.js'
            }
        },
        module: {
            rules: [
                { test: /\.html$/, loader: 'raw-loader', exclude: ['./src/index.html'] },
                { test: /\.ts$/, use: 'awesome-typescript-loader' },
                { test: /\.css$/, use: ['style-loader', 'css-loader'] },
                {
                    test: /\.scss$/, use: [
                        {
                            loader: "style-loader"
                        },
                        {
                            loader: "css-loader", options: { sourceMap: true }
                        },
                        {
                            loader: "sass-loader", options: { sourceMap: true }
                        }]
                }
            ]
        },
        plugins: [
            new webpack.SourceMapDevToolPlugin({
                filename: '[file].map', // Remove this line if you prefer inline source maps
                moduleFilenameTemplate: path.relative(bundleOutputDir, '[resourcePath]') // Point sourcemap entries to the original file locations on disk
            })
        ],
        stats: { modules: false },
        context: __dirname
        
};
