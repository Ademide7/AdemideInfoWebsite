import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import fs from 'fs';
import path from 'path';

let https;

if (process.env.NODE_ENV !== 'production') {
    const baseFolder =
        process.env.APPDATA && process.env.APPDATA !== ''
            ? `${process.env.APPDATA}/ASP.NET/https`
            : `${process.env.HOME}/.aspnet/https`;

    const certName = "AdemideInfoWebsite.Client";

    const certPath = path.join(baseFolder, `${certName}.pem`);
    const keyPath = path.join(baseFolder, `${certName}.key`);

    if (fs.existsSync(certPath) && fs.existsSync(keyPath)) {
        https = {
            key: fs.readFileSync(keyPath),
            cert: fs.readFileSync(certPath),
        };
    }
}

export default defineConfig({
    plugins: [react()],
    server: {
        https,
    },
});