import { environment } from "src/environments/environment";

export class ServerHelper
{
  static getApiUrl()
  {
    return environment.apiUrl;
  }
}
