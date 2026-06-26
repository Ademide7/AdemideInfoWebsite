import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import fs from "fs";
import path from "path";
import child_process from "child_process";
import { fileURLToPath, URL } from "node:url";
import { env } from "process";

const isVercel = !!process.env.VERCEL;

let https = undefined;

if (!isVercel) {
    const baseFolder =
        env.APPDATA && env.APPDATA !== ""
            ? `${env.APPDATA}/ASP.NET/https`
            : `${env.HOME}/.aspnet/https`;

    const certificateName = "ademideinfowebsite.client";
    const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
    const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

    if (!fs.existsSync(baseFolder)) {
        fs.mkdirSync(baseFolder, { recursive: true });
    }

    if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
        child_process.spawnSync("dotnet", [
            "dev-certs",
            "https",
            "--export-path",
            certFilePath,
            "--format",
            "Pem",
            "--no-password",
        ]);
    }

    https = {
        key: fs.readFileSync(keyFilePath),
        cert: fs.readFileSync(certFilePath),
    };
}

export default defineConfig({
    base: "/AdemideInfoWebsite/",
    plugins: [react()],
    resolve: {
        alias: {
            "@": fileURLToPath(new URL("./src", import.meta.url)),
        },
    },
    server: {
        https,
        port: 64934,
    },
});