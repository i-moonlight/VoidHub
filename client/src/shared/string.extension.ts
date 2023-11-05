export class StringExtension {

  static ConvertToBoolean(input: string): boolean | undefined {
    try {
        return JSON.parse(input.toLowerCase());
    }
    catch (e) {
        return undefined;
    }
  }

}
