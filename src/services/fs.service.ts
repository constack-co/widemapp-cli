import * as fs from 'fs/promises';
import { join, normalize } from 'path';
import mkdirp from 'mkdirp';
import { FileEditResponse } from '../apis/templates/get-tree-view-by-group-id.api.service';

export class FsService {
    async addOrEditFile(data: {
        path: string;
        fileName: string;
        contentAdd: string;
        fileEdits: FileEditResponse[];
    }) {
        const resolvedPath = normalize(join(process.cwd(), data.path));
        const completedPath = normalize(join(resolvedPath, data.fileName));

        await mkdirp(resolvedPath).then(async () => {
            await fs.access(completedPath).then(async() => {
                for (const fileEdit of data.fileEdits) {
                    await fs.readFile(completedPath, 'utf-8').then(async (filedata) => {
                        filedata = filedata.replace(fileEdit.placeholder, fileEdit.content);
                        await fs.writeFile(completedPath, filedata)
                        .then(() => {})
                        .catch(error => console.log(error));
                    });
                }
            }).catch(async () => {
                await fs.writeFile(completedPath, data.contentAdd)
                .then(() => {})
                .catch(error => console.log(error));
            })
        });
    }

    async editFile(data: {
        path: string;
        fileName: string;
        fileEdits: FileEditResponse[];
    }) {
        const completedPath = normalize(join(process.cwd(), data.path, data.fileName));

        await fs.access(completedPath).then(async() => {
            for (const fileEdit of data.fileEdits) {
                await fs.readFile(completedPath, 'utf-8').then(async (filedata) => {
                    filedata = filedata.replace(fileEdit.placeholder, fileEdit.content);
                    await fs.writeFile(completedPath, filedata)
                    .then(() => {})
                    .catch(error => console.log(error));
                });
            }
        }).catch(async () => {
            console.log(`${completedPath} should edit, but can't be found!`)
        })
    }
}